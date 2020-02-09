export class ApiRoutes {
  private which: string = "";
  private ApiUrl: string = "http://localhost:49493/api";
  public getRoute: string;
  public getByIdRoute: string;
  public postRoute: string;
  public updateRoute: string;
  public removeRoute: string;

  constructor(which: string) {
    this.which = which;
  }

  setVariables() {
    this.getRoute = `${this.ApiUrl}/${this.which}`;
    this.getByIdRoute = `${this.ApiUrl}/${this.which}/:id`;
    this.postRoute = `${this.ApiUrl}/${this.which}/novo`;
    this.updateRoute = `${this.ApiUrl}/${this.which}/:id/update`;
    this.removeRoute = `${this.ApiUrl}/${this.which}/:id/remove`;

    return this;
  }
}
