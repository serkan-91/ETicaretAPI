export class List_Product {
  id: string;
  name: string;
  stock: number;
  price: number;
  createdDate: Date;
  updatedDate: Date;
}
export class Pagination {
  page: number;
  size: number;

  constructor(page: number = 1, size: number = 5) {
    this.page = page;
    this.size = size;
  }
}
