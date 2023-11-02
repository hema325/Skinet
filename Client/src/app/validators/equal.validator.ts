import { AbstractControl, FormControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function compareTo(field: string, targetName?: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const firstControl = control;
        const secondControl = control.parent?.get(field);

        if (!firstControl?.value)
            return null;

        return firstControl.value == secondControl?.value ? null : { compareTo: { targetName: targetName || field } };
    }
}