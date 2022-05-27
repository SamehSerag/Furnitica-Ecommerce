using AngularProject.Data;
using AngularProject.Models;
using DotNetWebAPI.DTOs;
using Microsoft.EntityFrameworkCore;
namespace AngularAPI.Services
{
    public class CartRepository : ICartRepository
    {
        // DbContext Injecting
        readonly ShoppingDbContext _dbContext;
        public CartRepository(ShoppingDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // Add product to Cart method
        public void AddProductToCart(string userId, int ProductId)
        {
            string cartId = GetCart(userId);
            int quantity = 1;

            CartProduct existingCartProduct = _dbContext.CartProducts.FirstOrDefault(x => x.ProductId == ProductId && x.CartId == cartId);
            if (existingCartProduct != null)
            {
                existingCartProduct.Quantity += 1;
                _dbContext.Entry(existingCartProduct).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                CartProduct cartproduct = new CartProduct
                {
                    ProductId = ProductId,
                    CartId = cartId,
                    Quantity = quantity
                };
                _dbContext.CartProducts.Add(cartproduct);
                _dbContext.SaveChanges();
            }
        }


        // Get Cart by Id if Exist and if Not create a cart
        public string GetCart(string userId)
        {
            Cart cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == userId);

            if (cart != null)
            {
                return cart.CartId;
            }
            else
            {
                Cart shoppingCart = new()
                {
                    CartId = Guid.NewGuid().ToString(),
                    UserId = userId
                };

                _dbContext.Carts.Add(shoppingCart);
                _dbContext.SaveChanges();

                return shoppingCart.CartId;
            }
        }

        // To Remove a product from Cart
        public void RemoveCartItem(string userId, int productId)
        {
            string cartId = GetCart(userId);
            CartProduct cartProduct = _dbContext.CartProducts.FirstOrDefault(x => x.ProductId == productId && x.CartId == cartId);

            _dbContext.CartProducts.Remove(cartProduct);
            _dbContext.SaveChanges();
        }

        // Delete Only ONE product from the cart
        public void DeleteOneCartItem(string userId, int productId)
        {
            string cartId = GetCart(userId);
            CartProduct cartProduct = _dbContext.CartProducts.FirstOrDefault(x => x.ProductId == productId && x.CartId == cartId);

            cartProduct.Quantity -= 1;
            _dbContext.Entry(cartProduct).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        // Count produts in the cart
        public List<CartProductDto> GetCartItemCount(string userId)
        {
            string cartId = GetCart(userId);

            //if (!string.IsNullOrEmpty(cartId))
            //{
            //    int cartItemCount = _dbContext.CartProducts.Where(x => x.CartId == cartId).Sum(x => x.Quantity);

            //    return cartItemCount;
            //}
            //else
            //{
            //}
                return GetProductsAvailableInCart(cartId);
        }

        // Clear all items from the cart
        public List<CartProductDto>  ClearCart(string userId)
        {

            string cartId = GetCart(userId);
            List<CartProduct> cartItem = _dbContext.CartProducts.Where(x => x.CartId == cartId).ToList();

            if (!string.IsNullOrEmpty(cartId))
            {
                foreach (CartProduct item in cartItem)
                {
                    _dbContext.CartProducts.Remove(item);
                    _dbContext.SaveChanges();
                }
            }
            return GetProductsAvailableInCart(cartId);
        }

        // Delete the whole cart
        public void DeleteCart(string cartId)
        {
            Cart cart = _dbContext.Carts.Find(cartId);
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
        }



        // ############################### Product Data ###############################
        public Product GetProductData(int productId)
        {
            Product product = _dbContext.Products.FirstOrDefault(x => x.Id == productId);

            if (product != null)
            {
                _dbContext.Entry(product).State = EntityState.Detached;
                return product;
            }
            return null;
        }


        // ######################## Available Products in Cart ########################
        public List<CartProductDto> GetProductsAvailableInCart(string cartID)
        {
            var cartItemList = new List<CartProductDto>();
            List<CartProduct> cartItems = _dbContext.CartProducts.Where(x => x.CartId == cartID).ToList();

            foreach (CartProduct item in cartItems)
            {
                Product product = GetProductData(item.ProductId);
                CartProductDto objCartItem = new CartProductDto
                {
                    Product = product,
                    Quantity = item.Quantity
                };

                cartItemList.Add(objCartItem);
            }
            return cartItemList;
        }
    }
}


