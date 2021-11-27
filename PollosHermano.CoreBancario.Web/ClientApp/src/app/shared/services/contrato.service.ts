import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ContratoService {
  private http: HttpClient;
  private apiUrl: string;

  constructor(http: HttpClient, @Inject("API_URL") apiUrl: string) {
    this.http = http;
    this.apiUrl = apiUrl;
  }

  get httpOptions() {
    return {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    };
  }
  
  getById(id: any) {
    return this.http.get(`${this.apiUrl}/Contrato/${id}`);
  }

  deleteById(id: any) {
    return this.http.delete(`${this.apiUrl}/Contrato/${id}`);
  }
  
  get() {
    return this.http.get(`${this.apiUrl}/Contrato`);
  }

  getList() {
    return this.http.get(`${this.apiUrl}/Contrato/List`);
  }

  post(entity: any) {
    return this.http.post(`${this.apiUrl}/Contrato`, entity, this.httpOptions);
  }

  put(entity: any) {
    return this.http.put(`${this.apiUrl}/Contrato`, entity, this.httpOptions);
  }
}
