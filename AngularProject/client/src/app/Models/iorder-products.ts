import {IProduct} from '../Models/iproduct';
export interface IorderProducts {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
    product: IProduct;
}
