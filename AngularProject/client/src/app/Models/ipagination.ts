import { IProduct } from "./iproduct";

export interface IPagination {
    pageIndex: number;
    totalPages: number;
    totalCounts: number;
    pageSize: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    data: IProduct[];
}
