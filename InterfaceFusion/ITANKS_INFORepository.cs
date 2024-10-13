namespace InterfaceFusion
{
    public interface ITANKS_INFORepository
    {
        bool CheckTankTANK_INFO(int tank);

        long Insert(TANKS_INFO tanks_info);

        bool Update(TANKS_INFO tanks_info);

        long InsertHist(TANKS_INFO_HIST tanks_info_hist);
    }
}
