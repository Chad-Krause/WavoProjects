export class Team {
    id: number;
    name: string;
    color: string;

    constructor(obj: any) {
        Object.keys(obj).forEach(key => {
            this[key] = obj[key];
        });
    }

    get backgroundColor() {
        return this.color + "16";
    }
}