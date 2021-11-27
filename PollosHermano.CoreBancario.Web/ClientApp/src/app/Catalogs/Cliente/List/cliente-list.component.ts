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
import * as cAlert from "../../../custom/components/custom-alert/custom-alert";
import * as cConfirm from "../../../custom/components/custom-confirm/custom-confirm";
import { GenericResponse } from "../../../shared/models/common/GenericResponse";

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

  getData() {
    this.service.getList().subscribe((response: GenericResponse<any>) => {
      if (response &&
          response.status === 1 &&
          Utils.IsValidArray(response.data)) {
        this.data = response.data;
        this.filteredData = response.data;
      }
    },
    (error: any) => {

    },
    () => {

    });
  }

  ngOnInit() {
    this.getData();
  }

  delete(id: any) {
    cConfirm.show(2, "", "¿Éstas seguro que deseas eliminar este elemento?", () => {
        this.service.deleteById(id).subscribe((response: GenericResponse<any>) => {
        if (response &&
            response.status === 1 &&
            response.data) {
              
            this.getData();

            cAlert.show(1, "", "La informacion se eliminó correctamente", () => {
                
            });
        }
        },
        (error: any) => {
            cAlert.show(2, "", "Ocurrió un problema al eliminar la información", () => {

            });
        },
        () => {

        });
    }, () => {

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
