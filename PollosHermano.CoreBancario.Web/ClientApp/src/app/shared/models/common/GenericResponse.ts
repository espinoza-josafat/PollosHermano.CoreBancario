export interface GenericResponse<T> {
  status: number;
  message: string;
  description: string;
  data: T;
}
