export interface IOwnerProduct {
    id: number;
    title_EN: string;
    title_AR: string;
    details_EN: string;
    details_AR: string;
    price: number;
    color: number;
    quantity: number;
    category: Category;
    image: Image[];
    orderProduct: OrderProduct[];
}


export interface Category {
    id: number;
    name: string;
}

export interface Image {
    id: number;
    src: string;
}

export interface OrderProduct {
    id: number;
    orderId: number;
    quantity: number;
}
