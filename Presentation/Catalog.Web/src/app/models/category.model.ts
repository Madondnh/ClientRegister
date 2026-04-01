export interface Category{
  id: string;
  name: string;
  categoryCode: string; // Must match ABC123 format
  isActive: boolean;
  parentCategoryId: string | null;
  children: Category[] | null;
}