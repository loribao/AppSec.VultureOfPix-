class Store {
    public key: string;
    public value: unknown;
    public id: number;

    constructor({key, value}: {key: string, value: unknown}) {
        this.key = key;
        this.value = value;
        this.id = 0;
    }
}

export default Store;
