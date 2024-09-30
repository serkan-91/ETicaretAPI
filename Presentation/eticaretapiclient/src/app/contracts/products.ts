export interface Product {
  id: string;
  name: string;
  stock: number;
  price: number;
  createdDate: string;
  updatedDate: string;
  isDeleted?: boolean; // Optional property
}
