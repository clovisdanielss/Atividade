
using Atividade.Exceptions;
using Atividade.Models;
using Atividade.Repository;

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
                try
                {
                    sale = sales.Create(sale);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new SalesmanMustNotBeNullException().Message);
                }
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
                try
                {
                    sale = sales.Create(sale);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new AtLeastOneItemRequiredException().Message);
                }
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
                try
                {
                    var saleFound = sales.GetById("Qualquer Coisa");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ItemNotFoundException().Message);
                }
            }
        }
        [TestMethod]
        public void ChangeStatus_SuccessOnly()
        {
            var sale = CreateSale();
            Assert.IsTrue(sale.Status == SaleStatus.WAITING_PAYMENT);
            using (SalesRepository sales = new SalesRepository())
            {
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
        }
        [TestMethod]
        public void ChangeStatus_FailureOnly()
        {
            var sale = CreateSale();
            Assert.IsTrue(sale.Status == SaleStatus.WAITING_PAYMENT);
            using (SalesRepository sales = new SalesRepository())
            {
                //WAITING_PAYMENT
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.SENT);
                }
                catch(Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.SENT).Message);
                }
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.DELIVERED);
                }
                catch(Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.DELIVERED).Message);
                }

                //PAYMENT_APPROVED
                sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED);
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.WAITING_PAYMENT);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.WAITING_PAYMENT).Message);
                }
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.DELIVERED);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.DELIVERED).Message);
                }

                //SENT
                sales.ChangeStatus(sale, SaleStatus.SENT);
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.WAITING_PAYMENT);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.WAITING_PAYMENT).Message);
                }
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.CANCELLED);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.CANCELLED).Message);
                }
                try
                {
                    sales.ChangeStatus(sale, SaleStatus.PAYMENT_APPROVED);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, SaleStatus.PAYMENT_APPROVED).Message);
                }

                //DELIVERED
                sales.ChangeStatus(sale, SaleStatus.DELIVERED);
                for(int i=0; i<5; i++)
                {
                    try
                    {
                        sales.ChangeStatus(sale, (SaleStatus)i);
                    }
                    catch (Exception ex)
                    {
                        Assert.IsTrue(ex.Message == new ImpossibleToChangeStatusException(sale.Status, (SaleStatus)i).Message);
                    }
                }
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
                for (int i =0; i< 10; i++)
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
