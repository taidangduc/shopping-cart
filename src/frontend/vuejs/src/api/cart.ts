import apiClient from './api-client';
import type { CartItemRequest, CartResponse } from '@/definition/cart';

export async function getCart(): Promise<CartResponse> {
  const { data } = await apiClient.get<CartResponse>('/cart');
  return data;
}

export async function updateCart(items: CartItemRequest[]): Promise<CartResponse | null> {
  const { data } = await apiClient.post<CartResponse | null>('/cart', items);
  return data;
}

export async function deleteCart(): Promise<void> {
  await apiClient.delete('/cart');
}
