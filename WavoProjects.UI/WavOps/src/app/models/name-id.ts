export class NameId {
    name: string = "";
    id: number = 0;

    constructor(obj: any) {
        if(obj) {
            Object.keys(obj).forEach(key => {
                this[key] = obj[key];
            });
        }
    }
}