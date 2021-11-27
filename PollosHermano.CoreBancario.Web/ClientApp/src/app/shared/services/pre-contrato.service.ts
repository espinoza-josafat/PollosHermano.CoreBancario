import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class PreContratoService {
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
    return this.http.get(`${this.apiUrl}/PreContrato/${id}`);
  }

  deleteById(id: any) {
    return this.http.delete(`${this.apiUrl}/PreContrato/${id}`);
  }
  
  get() {
    return this.http.get(`${this.apiUrl}/PreContrato`);
  }

  getList() {
    return this.http.get(`${this.apiUrl}/PreContrato/List`);
  }

  post(entity: any) {
    return this.http.post(`${this.apiUrl}/PreContrato`, entity, this.httpOptions);
  }

  put(entity: any) {
    return this.http.put(`${this.apiUrl}/PreContrato`, entity, this.httpOptions);
  }
}
