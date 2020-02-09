import { Component } from "@angular/core";
import { DataService } from "./data.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title: string;
  activeRoute: string;

  constructor(private data: DataService) {
    this.data.currentTitle.subscribe(title => (this.title = title));

    this.data.activeRoute.subscribe(route => (this.activeRoute = route));
  }

  OnInit() {}
}
