export class ProductsSearchModel {
    minPrice: number = 0;
    maxPrice: number = -1;
    sortby: string = "";
    sortdir: string = "asc";
    category: (number)[] = [];
    search: string="";
    pageIndex: number = 1;
    pageSize: number = 12;
    color:number | null = null;
    ownerId:string | null = null;

    public toString = () : string => {
        var query:string = "?";
        if(this.ownerId != null)
            query+=`ownerId=${this.ownerId}`;
        if(this.maxPrice >= 0)
            query+=`minPrice=${this.minPrice}&maxPrice=${this.maxPrice}`;
        
        if(this.sortby != "" )
            query+=`&sortby=${this.sortby}&sortdir=${this.sortdir}`;

        if(this.search != "" )
            query+=`&search=${this.search}`;
        
        if(this.pageIndex != 1)
            query += `&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;

        if(this.color != null)
            query += `&color=${this.color}`;

        for (const cat of this.category) {
            query+= `&category=${cat}`;
        }
        return query;
    }
}