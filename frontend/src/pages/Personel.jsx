import { useState, useEffect } from "react";
import { useSetLoading } from "../components/Layout";
import { getAllProducts, createRequest } from "../utils/api";

const Personel = () => {
  const [products, setProducts] = useState([]);
  const setLoading = useSetLoading();

  useEffect(() => {
    setLoading(true);

    getAllProducts()
      .then((data) => {
        if (!data) throw new Error("Ürünler çekilemedi.");
        if (data.length === 0) throw new Error("Ürün bulunamadı.");
        setProducts(data);
      })
      .catch((error) => {
        alert(
          "Ürünleri çekerken bir hata oluştu. Lütfen tekrar deneyin. Hata: \n" +
            JSON.stringify(error),
        );
      })
      .finally(() => {
        setLoading(false);
      });
  }, [setLoading]);

  const onSubmit = async (event) => {
    event.preventDefault();
    try {
      const productId = parseInt(event.target.productId.value);
      const quantity = parseInt(event.target.quantity.value);
      const urgency = event.target.urgency.value;

      await createRequest({ productId, quantity, urgency });
      alert("Talep başarıyla oluşturuldu.");
    } catch (error) {
      alert(
        "Talep oluşturulurken bir hata oluştu. Lütfen tekrar deneyin. Hata: \n" +
          JSON.stringify(error),
      );
    }
  };

  return (
    <div className="flex items-center justify-center h-screen">
      <form
        className="flex flex-col gap-2 w-max items-center justify-center bg-gray-100 p-8 rounded shadow-md"
        onSubmit={onSubmit}
      >
        <h2 className="text-2xl font-bold mb-4">Satın Alma Talebi Oluştur</h2>
        <fieldset className="flex flex-col w-max">
          <label className="text-sm font-semibold text-gray-600" htmlFor="urgency">
            Ürün
          </label>
          <select
            id="productId"
            className="w-[200px] p-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500"
          >
            {products.map((product) => (
              <option key={product.Id} value={product.Id}>
                {product.Name} ({product.Price} TL)
              </option>
            ))}
          </select>
        </fieldset>
        <fieldset className="flex flex-col w-max">
          <label className="text-sm font-semibold text-gray-600" htmlFor="urgency">
            Adet
          </label>
          <input
            id="quantity"
            type="number"
            className="w-max p-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500"
            placeholder="Adet"
            defaultValue={1}
          />
        </fieldset>
        <fieldset className="flex flex-col w-max">
          <label className="text-sm font-semibold text-gray-600" htmlFor="urgency">
            Aciliyet
          </label>
          <select
            id="urgency"
            className="w-[200px] p-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500"
          >
            {["Normal", "Acil"].map((urgency) => (
              <option key={urgency} value={urgency}>
                {urgency}
              </option>
            ))}
          </select>
        </fieldset>
        <button
          type="submit"
          className="w-max bg-indigo-500 text-white py-2 px-4 rounded hover:bg-indigo-600 focus:outline-none"
        >
          Satın Alma Talebi Oluştur
        </button>
      </form>
    </div>
  );
};

export default Personel;
