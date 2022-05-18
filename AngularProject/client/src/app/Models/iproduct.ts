export interface IProduct {
    id: number;
    title_EN: string;
    title_AR: string;
    details_EN: string;
    details_AR: string;
    price: number;
    color: number;
    quantity: number;
    categoryID: number;
    category?: any;
    images?: any;
    orderProducts: any[];
}
