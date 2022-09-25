import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  constructor(private http: HttpClient) { }

  // login page requests
  postLogin(authCredentials: string) {
    const response = this.http.get("https://team7project2api.azurewebsites.net/login", {
      headers: {'Authorization': authCredentials},
      observe: "response",
      responseType: "json"
    })

    return response;
  }

  postUserCreation(customerData: string, accessToken: string) {
    const response = this.http.post(`https://team7project2api.azurewebsites.net/login`, customerData, {
      // header for testing only since create customer is stuck under auth
      headers: new HttpHeaders({
        Authorization: `${accessToken}`,
        "Content-Type": "application/json"
        
      }),
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  // product page requests
  getProducts(startRow: number, endRow: number, accessToken: string) {
    console.log(accessToken)
    const response = this.http.get(`https://team7project2api.azurewebsites.net/store/${startRow}/${endRow}`, {
      headers: {"Authorization": `Bearer ${accessToken}`},
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  getFilteredProducts(material: string, type: string, accessToken: string) {
    const response = this.http.get(`https://team7project2api.azurewebsites.net/store/filter/${material}/${type}`, {
      headers: {"Authorization": `Bearer ${accessToken}`},
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  getReviews(accessToken: string, itemId: string) {
    const response = this.http.get(`https://team7project2api.azurewebsites.net/review/item/${itemId}`, {
      headers: {"Authorization": `Bearer ${accessToken}`},
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  getUserProfile(customerId: number, accessToken: string) {
    const response = this.http.get(`https://team7project2api.azurewebsites.net/customer/${customerId}`, {
      headers: {"Authorization": `Bearer ${accessToken}`},
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  getOrderHistory(customerId: number, accessToken: string) {
    const response = this.http.get(`https://team7project2api.azurewebsites.net/transactions/${customerId}`, {
      headers: {"Authorization": `Bearer ${accessToken}`},
      observe: "response",
      responseType: "json"
    })

    return response;
  }
}
