import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";

@Component({
  selector: "custom-check",
  templateUrl: "./custom-check.component.html",
  styleUrls: ["./custom-check.component.scss"]
})
export class CustomCheckComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired {

  @Input() id: string;
  @Input() tag: string;
  @Input() model: boolean = false;
  @Output() modelChange = new EventEmitter<boolean>();
  @Input() visible: boolean = true;
  @Input() readonly: boolean = false;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;
  @Input() name: string = "";

  //@Input() visible: string = "true";

  @Input() metadata: any;


  control: FormControl = new FormControl("", []);



  @Output() change = new EventEmitter<any>();
  

  enabledLog: boolean = false;

  constructor() {

  }

  @ViewChild("underlyingElement") underlyingElement: ElementRef;

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomCheckComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
  }

  getBoolean(value: any) {
    return Utils.StringToBoolean(value);
  }

  ngOnInit() {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("ngOnInit");
    }
    
    this.addValidators();
  }

  addValidators() {
    //debugger
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }

    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  onChange(event: any) {
    if (this.enabledLog) {
      console.log("onChange");
    }

    this.change.emit({component: this, event: event});
  }

  getErrorMessage() {
    //let result = "";

    if (this.control.hasError("required")) {
      return "Ningún valor ingresado en un campo obligatorio";
    }

    return "";
  }

  getType() : string {
    return "CustomCheckComponent";
  }

  set value(value: boolean) {
    this.model = value;
    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: undefined });
  }

  get value(): boolean {
    return this.model;
  }
}
