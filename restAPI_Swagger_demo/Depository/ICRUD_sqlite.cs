using restAPI_Swagger_demo.Models;

namespace restAPI_Swagger_demo.Depository
{
    public interface ICRUD_sqlite
    {
        public Task<List<tBook>> Gets();
        public Task<tBook>? Get(int Id);
        public Task<string>? Post(tBook opp);
        public Task<string>? Put(int Id, tBook opp);
        public Task<string>? Patch(int Id, tBook opp);
        public Task<string> Delete(int Id);
        public Task<bool> Check(int Id);
    }
}
