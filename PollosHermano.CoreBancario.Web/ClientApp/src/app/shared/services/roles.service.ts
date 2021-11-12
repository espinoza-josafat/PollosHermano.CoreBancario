import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class RolesService {
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

  get() {
    return this.http.get(`${this.apiUrl}/Identity/Roles`);
  }

  getById(id: string) {
    return this.http.get(`${this.apiUrl}/Identity/Roles/${id}`);
  }

  post(entity: any) {
    return this.http.post(`${this.apiUrl}/Identity/Roles/Edit`, entity, this.httpOptions);
  }
}
