import {
    Component,
    OnInit,
    ViewChild,
    ViewEncapsulation
} from "@angular/core";
import {
    Router
} from "@angular/router";
import {
    ColumnMode,
    DatatableComponent
} from "@swimlane/ngx-datatable";
import Utils from "../../../shared/helpers/Utils";
import {
    ClienteService
} from "../../../shared/services/cliente.service";

@Component({
  selector: "app-cliente-list",
  templateUrl: "./cliente-list.component.html",
  styleUrls: [
    "../../../../assets/sass/libs/datatables.scss",
    "./cliente-list.component.scss"
  ],
  encapsulation: ViewEncapsulation.None
})
export class ClienteListComponent implements OnInit {
  public data: Array<any> = [];

  public columnMode = ColumnMode;

  @ViewChild(DatatableComponent) table: DatatableComponent;

  private filteredData: Array<any> = [];

  constructor(
    private router: Router,
    private service: ClienteService
  ) {

  }

  ngOnInit() {
    this.service.getList().subscribe((response: any) => {
      if (Utils.IsValidJsonObject(response) &&
        Utils.IsValidNumber(response.status) &&
        response.status === 1 &&
        Utils.IsValidArray(response.data) &&
        response.data.length > 0) {
        this.data = response.data;
        this.filteredData = response.data;
      }
    },
    (error: any) => {

    },
    () => {

    });
  }

  filterUpdate(event) {
    const value = event.target.value.toLowerCase();

    if (this.filteredData && this.filteredData.length > 0) {
      let keys = Object.keys(this.filteredData[0]);
      const temp = this.filteredData.filter(function (item) {
        for (let i = 0; i < keys.length; i++) {
          if (item[keys[i]] && item[keys[i]].toString().toLowerCase().indexOf(value) !== -1 || !value) {
            return true;
          }
        }
      });

      this.data = temp;
      this.table.offset = 0;
    }
  }
}
