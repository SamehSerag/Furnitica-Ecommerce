export class ProductToAdd {
    id?: number;
    title_EN!: string;
    title_AR!: string;
    details_EN!: string;
    details_AR!: string;
    price!: number;
    color: number = 1;
    quantity!: number;
    categoryID!: number;
    ownerId!: string;
    // ImagesFiles?: FormData = new FormData();
}
