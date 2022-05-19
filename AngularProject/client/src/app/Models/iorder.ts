import {IorderProducts} from '../Models/iorder-products';
export interface IOrder {
    userID: string;
    id: number;
    date: Date;
    totalPrice: number;
    state: number;
    user?: any;
    orderProducts: IorderProducts[];
}

