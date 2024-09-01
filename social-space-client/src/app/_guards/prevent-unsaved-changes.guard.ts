import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

// Route guard (add this guard to MemberEditComponent route in app.routes)
// CanDeactive Function
export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component, currentRoute, currentState, nextState) => {

  // even though we have many parameters, we only need access to component

  if(component.editFormTemRef?.dirty) {
    return confirm('Are you sure? Your unsaved data will be lost.')
  }
  return true;
  
};
