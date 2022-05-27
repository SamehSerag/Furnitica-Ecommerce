  export interface IReview {
        id?: number;
        userName?: string;
        userImg?: string;
        reviewBody?: string;
        createdDate?: Date;
        starsCount?: number;
        productId?: number;
    }

  export interface IReviewsPagination{
    pageIndex: number;
    totalPages: number;
    totalCounts: number;
    pageSize: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    data : IReview[];
  }

