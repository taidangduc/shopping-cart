import apiClient from './api-client';
import type { PaginatedResponse, Product } from '@/definition/product';

export async function getProducts(page = 1, pageSize = 6): Promise<PaginatedResponse<Product>> {
  const { data } = await apiClient.get<PaginatedResponse<Product>>('/product', {
    params: { page, pageSize },
  });
  return data;
}
