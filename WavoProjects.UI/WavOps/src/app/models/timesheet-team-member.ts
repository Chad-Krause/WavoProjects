export class TimesheetTeamMember {
    id: number = 0;
    name: string = "";
    clockedIn: boolean = false;

    constructor(obj: any) {
        if(obj) {
            Object.keys(obj).forEach(key => {
                this[key] = obj[key];
            });
        }
    }
}