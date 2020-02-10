import { ApiRoutes, IResponse } from "./classes";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private httpOptions: Object;
  private apiRoutes: ApiRoutes;

  constructor(private http: HttpClient) {
    this.httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };
  }

  setRoute(which: string) {
    this.apiRoutes = new ApiRoutes(which).setVariables();
  }

  apiGet(otherRoute?: string) {
    let url = this.apiRoutes.getRoute;
    if (otherRoute) {
      url = `${this.apiRoutes.ApiUrl}/${otherRoute}`;
    }
    return this.http.get<IResponse>(url);
  }

  apiGetById(id: number) {
    return this.http.get<IResponse>(
      this.apiRoutes.getByIdRoute.replace(":id", String(id))
    );
  }

  apiRemove(id: number) {
    return this.http.delete<IResponse>(
      this.apiRoutes.removeRoute.replace(":id", String(id))
    );
  }

  apiPost(data: any) {
    return this.http.post<IResponse>(
      this.apiRoutes.postRoute,
      JSON.stringify(data),
      this.httpOptions
    );
  }

  apiUpdate(data: any, id: number) {
    return this.http.put<IResponse>(
      this.apiRoutes.updateRoute.replace(":id", String(id)),
      JSON.stringify(data),
      this.httpOptions
    );
  }
}
