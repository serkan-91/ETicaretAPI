export class Product {
  id!: string;
  name?: string;
  stock?: number;
  price?: number;
  createdDate?: Date;
  updatedDate?: Date;
    isDeleted?: boolean;
}
export class Pagination {
  page: number;
  size: number;

  constructor(page: number = 1, size: number = 5) {
    this.page = page;
    this.size = size;
  }
}
export interface PagingResult<T> {
  items: T[];
  totalCount: number;
}

export interface ProductsResponse {
  pagingResult: PagingResult<Product>;
  items: Product[]; // Ekstra kolaylık sağlamak için
}
