import { Component, forwardRef, Input } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormGroup,
  NG_VALUE_ACCESSOR
} from '@angular/forms';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputFormComponent),
      multi: true
    }
  ]
})
export class InputFormComponent implements ControlValueAccessor {
  @Input() parentForm: FormGroup;
  @Input() fieldName: string;
  @Input() label: string;
  @Input() inputType: string;
  @Input() error: string;
  @Input() checkValidation?: boolean;
  @Input() placeHolder?: string = '';

  value: string = '';
  isDisabled: boolean;
  hidePassword?: boolean;
  onChange: (value: string) => void;
  onTouched: () => void;

  get formField(): AbstractControl {
    return this.parentForm.get(this.fieldName);
  }

  constructor() {
    this.hidePassword = this.inputType == 'password';
    this.value = '';
  }

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
