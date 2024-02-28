import { useState, useEffect } from "react";
import { useSetLoading } from "../components/Layout";
import { getAllProducts, getAllRequests, approveRequest, rejectRequest } from "../utils/api";

const PersonelAmiri = () => {
  const [requests, setRequests] = useState([]);
  const [products, setProducts] = useState([]);
  const setLoading = useSetLoading();

  useEffect(() => {
    setLoading(true);

    Promise.all([getAllProducts(), getAllRequests()])
      .then((data) => {
        if (!data[0]) throw new Error("Ürünler çekilemedi.");
        if (data[0].length === 0) throw new Error("Ürün bulunamadı.");
        setProducts(data[0]);

        if (!data[1]) throw new Error("Talepler çekilemedi.");
        setRequests(data[1].filter((r) => r.Status === 1));
      })
      .catch((error) => {
        alert(
          "Ürün ve Talepleri çekerken bir hata oluştu. Lütfen tekrar deneyin. Hata: \n" +
            JSON.stringify(error),
        );
      })
      .finally(() => {
        setLoading(false);
      });
  }, [setLoading]);

  const processRequest = (request, status) => async () => {
    try {
      await (status === "approve" ? approveRequest : rejectRequest)(request.Id);
      alert("Talep başarıyla " + (status === "approve" ? "onaylandı." : "reddedildi."));
      setRequests((prev) => prev.filter((r) => r.Id !== request.Id));
    } catch (error) {
      alert(
        "Talep işlenirken bir hata oluştu. Lütfen tekrar deneyin. Hata: \n" + JSON.stringify(error),
      );
    }
  };

  return (
    <div className="flex items-center justify-center h-screen">
      <div className="flex flex-col gap-2 w-max items-center justify-center bg-gray-100 p-8 rounded shadow-md">
        <h2 className="text-2xl font-bold mb-4">Talepleri Görüntüle</h2>
        {requests.length === 0 ? (
          <div className="text-xl p-4">Onay bekleyen talep bulunamadı.</div>
        ) : (
          <div className="flex flex-col w-max max-w-full gap-2 p-4">
            <div className="grid gap-2 grid-cols-5 mb-4 font-bold px-1 text-center">
              <div>Ürün</div>
              <div>Miktar</div>
              <div>Aciliyet</div>
              <div>Fiyat</div>
              <div>İşlem</div>
            </div>
            {requests.map((request) => (
              <div
                key={request.Id}
                className="grid gap-2 grid-cols-5 bg-gray-300 px-2 py-1 rounded-lg shadow-sm text-center"
              >
                <div>{products.find((p) => p.Id === request.ProductId)?.Name}</div>
                <div>{request.Quantity}</div>
                <div>{request.Urgency}</div>
                <div>{request.TotalPrice}</div>
                <div className="flex gap-1">
                  <button
                    className="bg-green-500 text-white px-4 py-1 rounded shadow-sm"
                    onClick={processRequest(request, "approve")}
                  >
                    Onayla
                  </button>
                  <button
                    className="bg-red-500 text-white px-4 py-1 rounded shadow-sm"
                    onClick={processRequest(request, "reject")}
                  >
                    Reddet
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default PersonelAmiri;
