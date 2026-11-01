export interface Product {
  id: string;
  name: string;
  price: number;
  imageUrl: string | null;
  stock: number;
}

export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}
