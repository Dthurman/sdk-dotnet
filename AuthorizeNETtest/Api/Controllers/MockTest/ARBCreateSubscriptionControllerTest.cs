namespace AuthorizeNet.Api.Controllers.MockTest
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;

    [TestFixture]
    public class ARBCreateSubscriptionTest : ApiCoreTestBase 
	{

	    [TestFixtureSetUp]
        public new static void SetUpBeforeClass()
        {
		    ApiCoreTestBase.SetUpBeforeClass();
	    }

	    [TestFixtureTearDown]
        public new static void TearDownAfterClass()
        {
		    ApiCoreTestBase.TearDownAfterClass();
	    }

	    [SetUp]
	    public new void SetUp() 
		{
		    base.SetUp();
	    }

	    [TearDown]
	    public new void TearDown() 
		{
		    base.TearDown();
	    }

        [Test]
	    public void MockARBCreateSubscriptionTest()
	    {
		    //define all mocked objects as final
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;
            var mockController = GetMockController<ARBCreateSubscriptionRequest, ARBCreateSubscriptionResponse>();
            var mockRequest = new ARBCreateSubscriptionRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                    subscription = ArbSubscriptionOne,
                };
            var mockResponse = new ARBCreateSubscriptionResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    subscriptionId = "1234",
                    messages = new messagesType{ resultCode = messageTypeOk, message = new messagesTypeMessage[]{new messagesTypeMessage{ code="I00001", text="Successful"}}},

                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();


            SetMockControllerExpectations<ARBCreateSubscriptionRequest, ARBCreateSubscriptionResponse, ARBCreateSubscriptionController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.subscriptionId);
            Assert.AreEqual(controllerResponse.messages.resultCode, mockResponse.messages.resultCode);
            Assert.AreEqual(((ANetApiResponse)(controllerResponse)).messages.message[0].code, mockResponse.messages.message[0].code);
            Assert.AreEqual(((ANetApiResponse)(controllerResponse)).messages.message[0].text, mockResponse.messages.message[0].text);
            LogHelper.info(Logger, "ARBCreateSubscription: Details:{0}", controllerResponse.subscriptionId);
	    }
    }
}
