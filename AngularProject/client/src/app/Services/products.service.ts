import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPagination } from '../Models/ipagination';
import { IProduct } from '../Models/iproduct';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private httpClinet: HttpClient) { }

  getAllProducts(): Observable<IPagination> {
    return this.httpClinet.get<IPagination>(`${environment.APIURL}/api/products`);
  }

  getProductsByCatId(catId: number): Observable<IPagination>{
    return this.httpClinet.get<IPagination>(`${environment.APIURL}/api/products?Category=${catId}`);
  }
  getProductById(prdId: number): Observable<IProduct>{
    return this.httpClinet.get<IProduct>(`${environment.APIURL}/api/products/${prdId}`);
  }

  addProduct(mewPrd: IProduct){

  }
  updateProduct(prdId: number, updatedProduct: IProduct){

  }
  deleteProduct(prdId: number){

  }
}
