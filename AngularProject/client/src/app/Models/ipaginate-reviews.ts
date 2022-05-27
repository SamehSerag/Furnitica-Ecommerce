import { IReview } from "./ireviews";

export interface IPagination {
  pageIndex: number;
  totalPages: number;
  totalCounts: number;
  pageSize: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  data: IReview[];
}
