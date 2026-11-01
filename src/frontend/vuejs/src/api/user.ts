import apiClient from './api-client';
import type { TokenRequest, TokenResponse, User } from '@/definition/user';

export async function login(request: TokenRequest): Promise<TokenResponse> {
  const { data } = await apiClient.post<TokenResponse>('/token', request);
  return data;
}

export async function getProfile(): Promise<User> {
  const { data } = await apiClient.get<User>('/user/me/profile');
  return data;
}
