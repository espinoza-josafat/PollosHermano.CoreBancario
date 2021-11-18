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
import { GenericResponse } from "../../../shared/models/common/GenericResponse";

import { ContratoService } from "../../../shared/services/contrato.service";
import { ClienteService } from "../../../shared/services/cliente.service";
import { CatTipoCuentaService } from "../../../shared/services/cat-tipo-cuenta.service";


import { CuentaService } from "../../../shared/services/cuenta.service";

@Component({
  selector: "app-cuenta-edit",
  templateUrl: "./cuenta-edit.component.html",
  styleUrls: ["./cuenta-edit.component.scss"]
})
export class CuentaEditComponent extends CustomBaseComponent implements OnInit {
  active = 1;
  formValidate: FormGroup;

  @ViewChildren(CustomComponentValidateDirective) validators: QueryList<CustomComponentValidateDirective>;

  constructor(
    private route: ActivatedRoute,
    private contratoService: ContratoService,
    private clienteService: ClienteService,
    private catTipoCuentaService: CatTipoCuentaService,

    private service: CuentaService
  ) {
    super();
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    forkJoin(
            this.contratoService.get(),
      this.clienteService.get(),
      this.catTipoCuentaService.get(),

    ).subscribe((response: Array<any>) => {

      const [IdContrato, IdCliente, IdTipoCuenta] = response;
      
      if (IdContrato &&
          IdContrato.status === 1 &&
        Utils.IsValidArray(IdContrato.data)) {
        this.setDataSource("IdContrato", IdContrato.data, this.validators);
      }


      if (IdCliente &&
          IdCliente.status === 1 &&
        Utils.IsValidArray(IdCliente.data)) {
        this.setDataSource("IdCliente", IdCliente.data, this.validators);
      }


      if (IdTipoCuenta &&
          IdTipoCuenta.status === 1 &&
        Utils.IsValidArray(IdTipoCuenta.data)) {
        this.setDataSource("IdTipoCuenta", IdTipoCuenta.data, this.validators);
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
        alert("La informacion se guardó correctamente");
      }
    },
    (error: any) => {

    },
    () => {

    });
  }
}
