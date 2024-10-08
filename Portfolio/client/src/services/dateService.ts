export abstract class DateString{
    static create = (format: string, date: Date) => {
        let dateString = ""
        if(format == dateFormat.yyyyMMdd){
            dateString = `${date.getFullYear()}-${this.getMonth(date)}-${this.getDate(date)}`
        }else if(format == dateFormat.ddMMyyyy){
            dateString = `${this.getDate(date)}/${this.getMonth(date)}/${date.getFullYear()}`
        }
        return dateString
    }

    private static getDate(date: Date){
        return date.getDate().toString().padStart(2, '0')
    }

    private static getMonth(date: Date){
        let month = date.getMonth();
        (month >= 0 && month < 12) ? month++ : month = 1
        return month.toString().padStart(2, '0')
    }
}

interface DateFormat{
    yyyyMMdd: string
    ddMMyyyy: string
}

export const dateFormat: DateFormat = {
    yyyyMMdd: "yyy-MM-dd",
    ddMMyyyy: "dd-MM-yyyy"
}