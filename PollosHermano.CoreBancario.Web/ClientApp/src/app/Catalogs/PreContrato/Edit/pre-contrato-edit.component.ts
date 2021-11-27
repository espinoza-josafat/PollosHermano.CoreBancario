import {
    Component,
    OnInit,
    QueryList,
    ViewChildren
} from "@angular/core";
import {
    FormGroup
} from "@angular/forms";
import {
    ActivatedRoute, Params
} from "@angular/router";
import { forkJoin } from "rxjs";
import { CustomBaseComponent } from "../../../custom/components/custom-base-component/CustomBaseComponent";
import { CustomComponentValidateDirective } from "../../../custom/directives/custom-component-validate.directive";
import Utils from "../../../shared/helpers/Utils";
import * as cAlert from "../../../custom/components/custom-alert/custom-alert";
import * as cConfirm from "../../../custom/components/custom-confirm/custom-confirm";
import { GenericResponse } from "../../../shared/models/common/GenericResponse";

import { VendedorService } from "../../../shared/services/vendedor.service";


import { PreContratoService } from "../../../shared/services/pre-contrato.service";

@Component({
  selector: "app-pre-contrato-edit",
  templateUrl: "./pre-contrato-edit.component.html",
  styleUrls: ["./pre-contrato-edit.component.scss"]
})
export class PreContratoEditComponent extends CustomBaseComponent implements OnInit {
  active = 1;
  formValidate: FormGroup;

  @ViewChildren(CustomComponentValidateDirective) validators: QueryList<CustomComponentValidateDirective>;

  constructor(
    private route: ActivatedRoute,
    private vendedorService: VendedorService,

    private service: PreContratoService
  ) {
    super();
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    forkJoin(
            this.vendedorService.get(),

    ).subscribe((response: Array<any>) => {

      const [IdVendedor] = response;
      
      if (IdVendedor &&
          IdVendedor.status === 1 &&
        Utils.IsValidArray(IdVendedor.data)) {
        this.setDataSource("IdVendedor", IdVendedor.data, this.validators);
      }



    },
    (error: any) => {

    },
    () => {
      this.editData();
    });
  }

  editData() {
    this.route.params.subscribe((params: Params) => {
      const id = params["id"];
      if (id) {
        this.getEntity(id);
      }
    });
  }

  getEntity(id: any) {
    this.service.getById(id).subscribe((response: GenericResponse<any>) => {
      if (response &&
          response.status === 1 &&
          Utils.IsValidJsonObject(response.data)) {
        this.populateModel(response.data, this.validators);
      }
    },
    (error: any) => {

    },
    () => {

    });
  }

  ngAfterViewInit() {
    const validators: any = {};

    this.validators.forEach((x: CustomComponentValidateDirective) => {
      if (Utils.IsValidStringNotEmpty(x.component.name) && x.component.control) {
        validators[x.component.name] = x.component.control;
      }
    });

    this.formValidate = new FormGroup(validators);

    this.getData();
  }

  save() {
    const isValid = this.formValidate.valid;
    if (!isValid) {
      return;
    }

    const model = this.generateModel(this.validators);

    this.service.post(model).subscribe((response: GenericResponse<any>) => {
      if (response &&
        response.status === 1 &&
        Utils.IsValidJsonObject(response.data)) {
        this.setValue("Id", response.data.id, this.validators);
        cAlert.show(1, "", "La informacion se guardó correctamente", () => {

        });
      }
    },
    (error: any) => {
        cAlert.show(2, "", "Ocurrió un problema al guardar la información", () => {

        });
    },
    () => {

    });
  }
}
