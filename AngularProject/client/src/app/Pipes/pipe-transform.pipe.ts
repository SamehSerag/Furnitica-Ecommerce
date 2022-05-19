import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'pipeTransform'
})
export class PipeTransformPipe implements PipeTransform {

  transform(value: string, limit =30, completeWords=false, ellipsis = '...'): string {
    if (completeWords) {
      limit = value.slice(0, limit).lastIndexOf(' ');
    }
    return value.length > limit ? value.slice(0, limit) + ellipsis : value;
  }

}
