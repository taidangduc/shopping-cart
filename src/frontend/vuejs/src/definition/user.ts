export interface User {
  id: string;
  email: string;
  username: string;
}

export interface TokenRequest {
  email: string;
  password: string;
}

export interface TokenResponse {
  access_token: string;
  token_type: string;
  expires_in: number;
}
