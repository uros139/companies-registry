import { NgModule } from "@angular/core";
import { API_BASE_URL } from "./api-reference";
import { environment } from "../../environments/environment";

@NgModule({
  declarations: [],
  imports: [],
  providers: [{
    provide: API_BASE_URL,
    useValue: environment.apiUrl
  }]
})
export class ApiModule { }