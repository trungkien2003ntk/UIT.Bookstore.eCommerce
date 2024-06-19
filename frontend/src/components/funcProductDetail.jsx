export const skusToOptions = (skus) => {
  const optionsMap = new Map()

  skus.forEach((sku) => {
    sku.optionValues.forEach((option) => {
      if (!optionsMap.has(option.name)) {
        optionsMap.set(option.name, { title: option.name, items: [] })
      }
      optionsMap.get(option.name).items.push({
        name: option.value,
        image: sku.largeImageUrl,
        basicDiscountRate: sku.basicDiscountRate,
        quantity: sku.quantity,
        recommendedRetailPrice: sku.recommendedRetailPrice,
        unitPrice: sku.unitPrice,
        id: sku.id,
      })
    })
  })

  return Array.from(optionsMap.values())
}
