import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DataService {
  private titleSource = new BehaviorSubject<string>("Angular");
  private routeSource = new BehaviorSubject<string>("");

  currentTitle = this.titleSource.asObservable();
  activeRoute = this.routeSource.asObservable();

  constructor() {}

  changeTitle(title: string) {
    this.titleSource.next(title);
  }

  changeRoute(route: string) {
    this.routeSource.next(route);
  }
}
