namespace PublicTransportManagementSystem
{
    public class Bus
    {
        //public Bus(string id, string number, int capacity)
        //{
        //    this.Id = id;
        //    this.Number = number;
        //    this.Capacity = capacity;
        //}

        public string Id { get; set; }
    
        public string Number { get; set; }
    
        public int Capacity { get; set; }

        public override string ToString()
        {
            return this.Id;
        }
    }
}