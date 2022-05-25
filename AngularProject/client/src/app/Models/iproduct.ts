export interface IProduct {
    id: number;
    title_EN: string;
    title_AR: string;
    details_EN: string;
    details_AR: string;
    price: number;
    color: string;
    quantity: number;
    categoryID: number;
    category?: any;
    images?: any;
    rating:number;
    orderProducts: any[];
}
