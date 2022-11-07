using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .Property(i => i.AccountScope)
                .HasComputedColumnSql(
                    @$"
                        CASE
                            WHEN [{nameof(Account.BusinessId)}] IS NULL
                            THEN CASE
		                        WHEN [{nameof(Account.BusinessTypeId)}] IS NULL
		                        THEN CAST({AccountScope.Global:d} AS INT)
		                        ELSE CAST({AccountScope.BusinessTypeSpecific:d} AS INT)
	                        END
	                        ELSE CAST({AccountScope.Local:d} AS INT)
                        END
                    ");
        }
    }
}
