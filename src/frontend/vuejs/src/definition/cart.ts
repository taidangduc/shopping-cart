export interface CartProduct {
  id: string;
  name: string;
  price: number;
  imageUrl: string | null;
  quantity: number;
}

export interface CartItemRequest {
  id: string;
  quantity: number;
}

export interface CartResponse {
  id: string;
  items: CartProduct[];
}
