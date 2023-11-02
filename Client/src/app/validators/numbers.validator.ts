import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function numbers(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (!control.value)
            return null;

        console.log(/^[0-9]*$/.test(control.value) ? null : { "numbers": 'false' });
        return /^[0-9]*$/.test(control.value) ? null : { "numbers": 'false' };
    }
}