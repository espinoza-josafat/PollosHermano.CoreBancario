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

import { PreContratoService } from "../../../shared/services/pre-contrato.service";


import { ContratoService } from "../../../shared/services/contrato.service";

@Component({
  selector: "app-contrato-edit",
  templateUrl: "./contrato-edit.component.html",
  styleUrls: ["./contrato-edit.component.scss"]
})
export class ContratoEditComponent extends CustomBaseComponent implements OnInit {
  active = 1;
  formValidate: FormGroup;

  @ViewChildren(CustomComponentValidateDirective) validators: QueryList<CustomComponentValidateDirective>;

  constructor(
    private route: ActivatedRoute,
    private preContratoService: PreContratoService,

    private service: ContratoService
  ) {
    super();
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    forkJoin(
            this.preContratoService.get()

    ).subscribe((response: Array<any>) => {

      const [IdPreContrato] = response;
      
      if (IdPreContrato &&
          IdPreContrato.status === 1 &&
        Utils.IsValidArray(IdPreContrato.data)) {
        this.setDataSource("IdPreContrato", IdPreContrato.data, this.validators);
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
