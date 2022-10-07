
using Atividade.Models;
using Atividade.Repository;
using Atividade.Services;

namespace Teste_da_Atividade
{
    [TestClass]
    public class SalesTest
    {
        [TestMethod]
        public Sale CreateSale()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                Sale sale = new Sale
                {
                    Cart = new Cart
                    {
                        Items = new List<Item>
                        {
                            new Item
                            {
                                Name = "teste",
                                Price = 2
                            }
                        }
                    },
                    SalesDate = DateTime.Now,
                    Salesman = new Salesman
                    {
                        Cpf = "000.000.000-00",
                        Nome = "Fulano de tal",
                    }
                };
                sale = sales.Create(sale);
                Assert.IsTrue(sale != null);
                Assert.IsTrue(sale.Id != null);
                return sale;
            }
        }

        [TestMethod]
        public void CreateSale_WithoutSalesman()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                Sale sale = new Sale
                {
                    Cart = new Cart
                    {
                        Items = new List<Item>
                        {
                            new Item
                            {
                                Name = "teste",
                                Price = 2
                            }
                        }
                    },
                    SalesDate = DateTime.Now
                };
                sale = sales.Create(sale);
                Assert.IsNull(sale);
            }
        }
        [TestMethod]
        public void CreateSale_WithZeroItens()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                Sale sale = new Sale
                {
                    Cart = new Cart
                    {
                        Items = new List<Item>
                        {
                        }
                    },
                    Salesman = new Salesman
                    {
                        Cpf = "000.000.000-00",
                        Nome = "Fulano de tal",
                    },
                    SalesDate = DateTime.Now
                };
                sale = sales.Create(sale);
                Assert.IsNull(sale);
            }
        }

        [TestMethod]
        public void GetSale()
        {
            Sale sale = CreateSale();
            using (SalesRepository sales = new SalesRepository())
            {
                var saleFound = sales.GetById(sale.Id);
                Assert.IsNotNull(saleFound);
            }

        }

        [TestMethod]
        public void GetSale_NotFound()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                var saleFound = sales.GetById("Qualquer Coisa");
                Assert.IsNull(saleFound);
            }
        }
        [TestMethod]
        public void ChangeStatus_SuccessOnly()
        {
            var sale = CreateSale();
            Assert.IsTrue(sale.Status == SaleStatus.WAITING_PAYMENT);
            var sales = new SalesService();
            sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED);
            Assert.IsTrue(sale.Status == SaleStatus.PAYMENT_APPROVED);
            sales.ChangeStatus(sale, SaleStatus.SENT);
            Assert.IsTrue(sale.Status == SaleStatus.SENT);
            sales.ChangeStatus(sale, SaleStatus.DELIVERED);
            Assert.IsTrue(sale.Status == SaleStatus.DELIVERED);

            sale = CreateSale();
            sales.ChangeStatus(sale, SaleStatus.CANCELLED);
            Assert.IsTrue(sale.Status == SaleStatus.CANCELLED);

            sale = CreateSale();
            sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED);
            Assert.IsTrue(sale.Status == SaleStatus.PAYMENT_APPROVED);
            sales.ChangeStatus(sale, SaleStatus.CANCELLED);
            Assert.IsTrue(sale.Status == SaleStatus.CANCELLED);
        }

        [TestMethod]
        public void ChangeStatus_FailureOnly()
        {
            var sale = CreateSale();
            Assert.IsTrue(sale.Status == SaleStatus.WAITING_PAYMENT);
            var sales = new SalesService();
            //WAITING_PAYMENT
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.SENT));
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.DELIVERED));

            //PAYMENT_APPROVED
            sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED);
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.WAITING_PAYMENT));
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.DELIVERED));
            sales.ChangeStatus(sale, SaleStatus.SENT);

            //SENT
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.WAITING_PAYMENT));
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.CANCELLED));
            Assert.IsFalse(sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED));

            //DELIVERED
            sales.ChangeStatus(sale, SaleStatus.DELIVERED);
            for (int i = 0; i < 5; i++)
            {
                Assert.IsFalse(sales.ChangeStatus(sale, (SaleStatus)i));
            }

        }

        [TestMethod]
        public void GetAllSales()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                var sls = sales.GetAll();
                while (sls.Count > 0)
                    sales.DeleteById(sls[0].Id);
                for (int i = 0; i < 10; i++)
                {
                    sls = sales.GetAll();
                    Assert.IsNotNull(sls);
                    Assert.IsTrue(sls.Count == i);
                    CreateSale();
                }
            }
        }

        [TestMethod]
        public void DeleteSale()
        {
            using (SalesRepository sales = new SalesRepository())
            {
                Sale s = CreateSale();
                var sls = sales.GetAll();
                Assert.IsTrue(sls.Count > 1);
                while (sls.Count > 0)
                    sales.DeleteById(sls[0].Id);
                sls = sales.GetAll();
                Assert.IsTrue(sls.Count == 0);
            }
        }

    }


}
