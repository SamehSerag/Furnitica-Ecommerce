  export interface IReview {
        id: number;
        userId: string;
        userName: string;
        userImg: string;
        reviewBody: string;
        createdDate: Date;
        starsCount: number;
        productId: number;
    }
