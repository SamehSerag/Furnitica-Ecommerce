import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'asList'
})
export class AsListPipe implements PipeTransform {

  transform(value: number): any{

    return new Array(value);
  }

}
